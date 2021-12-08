import {instance} from "@/router/instance";
import router from "@/router/router";


export const eventModule = {
    state: () => ({
        events: [],
        allEvents: [],
        event: {
            eventName: "",
            startEventDate : new Date().toISOString().slice(0,-8),
            endEventDate: new Date().toISOString().slice(0,-8),
            shortDescription: "",
            description: "",
            status: 0
        },
        isLoading: false,
        selectedSort: '',
        searchQuery: '',
        limit: 25,
        defaultRoot: 'events',
        sortOptions: [
            {value: 'eventName', name: 'By name'},
            {value: 'startEventDate', name: 'By date'},
            {value: 'status', name: 'By status'},
        ],
    }),
    getters: {
        sortedEvents(state){
            const events = [...state.events, ...state.allEvents]
            const sortedList =  [...events].sort((event_a, event_b) =>
                event_a[state.selectedSort]?.toString().localeCompare(event_b[state.selectedSort]))
            state.events = sortedList.splice(0, state.events.length)
            state.allEvents = sortedList
            return state.events
        },
        sortedAndSearchedEvents(state, getters){
            return getters.sortedEvents.filter(e =>
                e.eventName.toLowerCase().includes(state.searchQuery.toLowerCase()))
        },
    },
    mutations: {
        setEvents(state, events){
            state.events = events;
        },
        setAllEvents(state, events){
            state.allEvents = events;
        },
        setEvent(state, event){
            state.event = event;
        },
        pushEvent(state, event){
            state.events.push(event);
        },
        setLoading(state, bool){
            state.isLoading = bool;
        },
        setSelectedSort(state, selectedSort){
            state.selectedSort = selectedSort;
        },
        setSearchQuery(state, searchQuery){
            state.searchQuery = searchQuery;
        },
        setDefaultRoot(state, defaultRoot){
            state.defaultRoot = defaultRoot
        },
        setSortedEvents(state, sortedEvents){
            state.sortedEvents = sortedEvents
        },
        clearEventStore(state){
            state.events = []
            state.allEvents = []
        },
        clearEvent(state){
            state.event = {
                eventName: "",
                startEventDate: new Date().toISOString().slice(0,-8),
                endEventDate: new Date().toISOString().slice(0,-8),
                shortDescription: "",
                description: "",
                status: 0
            }
        },
        assignUser(state, user){
            state.event.users.push(user)
        }

    },
    actions: {
        async createEvent({state, commit, rootState, rootGetters}) {
            rootState.errors = []
            await instance
                .post(`${state.defaultRoot}`, state.event, {headers: rootGetters.getHeaders})
                .then(response => {
                    state.event.id = response.data
                    commit('pushEvent', state.event)
                    commit('clearEvent')
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                    router.push('/login')
                })
        },
        async getEventList({commit, rootState, dispatch, rootGetters}, path) {
            await commit('clearEventStore')
            await commit('setLoading', true)
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(res => {
                    commit('setAllEvents', res.data)
                    dispatch('loadMoreEvents')
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
                .then(() => {
                    commit('setLoading', false)
                    if(rootState.errors.length !== 0)
                        router.push('/login')
                })
        },
        async loadMoreEvents({state, commit, dispatch}){
            const events = await dispatch('getMoreEvents')
            commit('setEvents' , [...state.events, ...events])
        },
        async getMoreEvents({state}, len=state.limit){
            if(state.allEvents.length >= len)
                return state.allEvents.splice(0, len)
            else {
                return state.allEvents.splice(0, state.allEvents.length)
            }
        },
        async getEvent({state, commit, rootState, rootGetters}, event_id){
            const path = `${state.defaultRoot}/${event_id}`
            rootState.errors = []
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(res => {
                    commit('setEvent', res.data)
                })
                .catch(error => {
                    console.log(error)
                    console.log('error')
                    if(error.headers.status === 401) {
                        rootState.errors.push("You are not a participant in the event")
                        router.push('/login')
                    }
                    else
                        router.back()
                })
            return state.event
        },
        async updateEvent({state, rootState, rootGetters}) {
            rootState.errors = []
            const path = `${state.defaultRoot}/${state.event.id}/update`
            await instance
                .put(path, state.event, {headers: rootGetters.getHeaders})
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async removeEvent({state, commit, rootState, rootGetters}, event_id){
            const path = `${state.defaultRoot}/${event_id}/delete`
            await instance.delete(path, {headers: rootGetters.getHeaders})
                .then(() => {
                    commit('setEvents', state.events.filter(x => x.id !== event_id ))
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async assignToEvent({state, commit, rootState, rootGetters}, params){
            const path = `${state.defaultRoot}/assign`
            rootState.errors = []
            await instance
                .put(path, {
                    Email: params[0],
                    EventId: params[1]
                    },
                    {headers: rootGetters.getHeaders})
                .then(() => commit('assignUser', {email: params[0]}))
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        }
    },
    namespaced: true

}