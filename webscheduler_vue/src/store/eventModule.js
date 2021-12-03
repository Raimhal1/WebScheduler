import {instance} from "@/router/instance";
import router from "@/router/router";

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
        file_ids: [],
        imageBlobs: [],
        textBlobs: [],
        isLoading: false,
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
                startEventDate: null,
                endEventDate: null,
                shortDescription: "",
                description: ""
            }
        },
        clearBlobs(state){
            state.imageBlobs = []
            state.textBlobs = []
        },
        addBlob(state, blob){
            if(blob.type.includes('image/'))
                state.imageBlobs.push(blob)
            else
                state.textBlobs.push(blob)
        },
        setFileIds(state, ids){
            state.file_ids = ids
        }
    },
    actions: {
        async createEvent({state, commit, dispatch, rootState, rootGetters}) {
            let event_id
            await instance
                .post('events', state.event, {headers: rootGetters.getHeaders})
                .then(res => {
                    event_id = res.data
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
                .then(() => {
                    if(rootState.errors.length !== 0)
                        router.push('/login')
                })
            const event = await dispatch('getEvent', event_id)
            commit('pushEvent', event)
            commit('clearEvent')
        },
        async getEventList({commit, rootState, dispatch, rootGetters}, path) {
            await commit('clearEventStore')
            await commit('setLoading', true)
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(res => {
                    commit('setAllEvents', res.data)
                    dispatch('loadMoreEvents')
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
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
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(res => {
                    commit('setEvent', res.data)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
                .then(() => {
                    if(rootState.errors.length !== 0)
                        router.push('/login')
                })
            return state.event
        },
        async updateEvent({state, rootState, rootGetters}) {
            console.log(state.event.id)
            const path = `${state.defaultRoot}/${state.event.id}/update`
            await instance
                .put(path, state.event, {headers: rootGetters.getHeaders})
                .then(() => {

                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
                })
            console.log('ok')
        },
        async removeEvent({state, commit, rootState, rootGetters}, event_id){
            const path = `${state.defaultRoot}/${event_id}/delete`
            await instance.delete(path, {headers: rootGetters.getHeaders})
                .then( res => {
                    console.log(res)
                    rootState.errors = []

                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
            commit('setEvents', state.events.filter(x => x.id !== event_id ))
        },
        async getEventFiles({state, commit, dispatch, rootState, rootGetters}){
            await commit('setLoading', true)
            await dispatch('getEventsFilesIds')
            await state.file_ids.forEach(id => {
                const path = `${state.defaultRoot}/${state.event.id}/files/${id}`
                instance
                    .get(path, {
                        responseType: 'blob',
                        headers: rootGetters.getHeaders
                    })
                    .then(response => {
                        const blob = new Blob(
                            [response.data],
                            {
                                type: response.headers['content-type']
                            })
                        blob.id = id
                        console.log(blob)
                        commit('addBlob', blob)
                    })
                    .catch(error => {
                        console.log(error.message)
                        rootState.errors.push(error)
                    })
                    .then(() => {
                        if(rootState.errors.length !== 0)
                            router.push('/login')
                    })
            })
            await commit('setLoading', false)
        },
        async getEventsFilesIds({state, commit, rootState, rootGetters}){
            const path = `${state.defaultRoot}/${state.event.id}/files`
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(response => {
                    commit('setFileIds', response.data)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })

        },
        async removeFile({state, dispatch, rootState, rootGetters}, file_id){
            const path = `${state.defaultRoot}/${state.event.id}/files/${file_id}/delete`
            await instance
                .delete(path, {headers: rootGetters.getHeaders})
                .then(response => {
                    console.log(response)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
                .then(() => {
                    dispatch('setBlobs',
                        [...state.imageBlobs, ...state.textBlobs]
                            .filter(blob => blob.id !== file_id )
                    )
                })
        },
        async setBlobs({commit}, blobs){
            commit('clearBlobs')
            blobs.forEach(blob => commit('addBlob', blob))
        }
    },
    namespaced: true

}