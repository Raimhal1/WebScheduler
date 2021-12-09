import {instance} from "@/router/instance";
import router from "@/router/router";


export const eventModule = {
    state: () => ({
        events: [],
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
        page: 0,
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
            return [...state.events].sort((event_a, event_b) =>
                event_a[state.selectedSort]?.toString().localeCompare(event_b[state.selectedSort]))
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
        addEvents(state, events){
            state.events = [...state.events, ...events];
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
            state.page = 0
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
        async getEventList({state, commit, rootState, rootGetters}, path) {
            if(state.events.length === 0)
                commit('setLoading', true)
            state.page += 1
            await instance
                .get(path, {
                    params: {
                        skip: (state.page - 1) * state.limit,
                        take: state.limit
                    },
                    headers: rootGetters.getHeaders})
                .then(res => {
                    commit('addEvents', res.data)
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