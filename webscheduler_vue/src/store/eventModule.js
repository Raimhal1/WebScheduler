import {instance} from '@/instance'

export const eventModule = {
    state: () => ({
        events: [],
        allEvents: [],
        sortedEvents: [],
        event: {
            eventName: "",
            startEventDate: "",
            endEventDate: "",
            shortDescription: "",
            description: "",
        },
        isEventListLoading: false,
        selectedSort: '',
        searchQuery: '',
        limit: 25,
        defaultRoot: 'events',
        sortOptions: [
            {value: 'eventName', name: 'By name'},
            {value: 'startEventDate', name: 'By date'}
        ]
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
        }
    },
    actions: {
        async createEvent({state, commit, dispatch}) {
            const response = await instance.post('events', state.event)
            const event_id = response.data
            const event = await dispatch('getEvent', event_id)
            commit('pushEvent', event)
            commit('setEvent',  {
                eventName: "",
                startEventDate: null,
                endEventDate: null,
                shortDescription: "",
                description: ""
            })
        },
        async getEventList({commit, dispatch}, path) {
            try {
                commit('setLoading', true)
                const result = await instance.get(path)
                commit('setAllEvents', result.data)
                commit('setEvents', [])
                await dispatch('loadMoreEvents')

            }
            catch (ex){
                console.log(ex.message)
            }
            finally {
                commit('setLoading', false)
            }
        },
        async loadMoreEvents({state, commit, dispatch}){
            const events = await dispatch('getMoreEvents')
            commit('setEvents' , [...state.events, ...events])
        },
        async getMoreEvents({state}, len=state.limit){
            console.log(state.allEvents.length)
            if(state.allEvents.length >= len)
                return state.allEvents.splice(0, len)
            else {
                return state.allEvents.splice(0, state.allEvents.length)
            }
        },
        async getEvent({state}, event_id){
            const path = `${state.defaultRoot}/${event_id}`
            const result = await instance.get(path)
            return result.data
        },
        async removeEvent({state, commit}, event_id){
            const path = `${state.defaultRoot}/${event_id}/delete`
            await instance.delete(path)
            commit('setEvents', state.events.filter(x => x.id !== event_id ))
        },
    },
    namespaced: true

}