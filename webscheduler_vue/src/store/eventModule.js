import {instance} from '@/instance'

export const eventModule = {
    state: () => ({
        events: [],
        allEvents: [],
        event: {
            eventName: "",
            startEventDate: "",
            endEventDate: "",
            shortDescription: "",
            description: "",
        },
        event_id: "",
        isEventListLoading: false,
        selectedSort: '',
        searchQuery: '',
        limit: 25,
        defaultRoot: 'events',
        sortOptions: [
            {value: 'eventName', name: 'By name'},
            {value: 'startEventDate', name: 'By date'}
        ],
    }),
    getters: {
        sortedEvents(state){
            const events = [...state.events, ...state.allEvents]
            const sortedList =  [...events].sort((event_a, event_b) =>
                event_a[state.selectedSort]?.localeCompare(event_b[state.selectedSort]))
            state.events = sortedList.splice(0, state.events.length)
            state.allEvents = sortedList
            return state.events
        },
        sortedAndSearchedEvents(state, getters){
            return getters.sortedEvents.filter(e => e.eventName.toLowerCase().includes(state.searchQuery.toLowerCase()))
        }
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
            state.isEventListLoading = bool;
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
                startEventDate: null,
                endEventDate: null,
                shortDescription: "",
                description: ""
            }
        }
    },
    actions: {
        async createEvent({state, commit, dispatch, rootState, rootGetters}) {
            let event_id
            await instance
                .post('events', state.event, rootGetters.getHeaders)
                .then(res => {
                    event_id = res.data
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
                })
            const event = await dispatch('getEvent', event_id)
            commit('pushEvent', event)
            commit('clearEvent')
        },
        async getEventList({commit, rootState, dispatch, rootGetters}, path) {
            await commit('setLoading', true)
            await instance
                .get(path, rootGetters.getHeaders)
                .then(res => {
                    commit('setLoading', true)
                    commit('setAllEvents', res.data)
                    dispatch('loadMoreEvents')
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
                })
            commit('setLoading', false)
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
            await instance
                .get(path, rootGetters.getHeaders)
                .then(res => {
                    commit('setEvent', res.data)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
                })
            return state.event
        },
        async updateEvent({state, rootState, rootGetters}) {
            console.log(state.event.id)
            const path = `${state.defaultRoot}/${state.event.id}/update`
            await instance
                .put(path, state.event, rootGetters.getHeaders)
                .then(res => {
                    res.data
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors = [...rootState.errors, error]
                })
        },
        async removeEvent({state, commit, rootState, rootGetters}, event_id){
            const path = `${state.defaultRoot}/${event_id}/delete`
            await instance.delete(path, rootGetters.getHeaders)
                .then( res => {
                    console.log(res)
                    rootState.errors = []

                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
                })
            commit('setEvents', state.events.filter(x => x.id !== event_id ))
        },
    },
    namespaced: true

}