import {instance} from '@/instance'
import router from "../router/router";

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
        files: [],
        urls: [],
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
        addEventFile(state, file){
            state.files = [...state.files, file]
        },
        setEventFiles(state, files){
            state.files = files
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
        clearFiles(state){
            state.files = []
            state.urls = []
        },
        clearUrls(state){
            state.urls = []
        },
        addFileUrl(state, url){
            state.urls.push(url)
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
                .then(() => {
                    if(rootState.errors.length !== 0)
                        router.push('/login')
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
                    commit('setAllEvents', res.data)
                    dispatch('loadMoreEvents')
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
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
                .get(path, rootGetters.getHeaders)
                .then(res => {
                    commit('setEvent', res.data)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
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
        async getEventFiles({state, commit, dispatch, rootState, rootGetters}){
            await commit('setLoading', true)
            const ids = await dispatch('getEventsFilesIds')
            await ids.forEach(id => {
                const path = `${state.defaultRoot}/${state.event.id}/files/${id}`
                instance
                    .get(path, rootGetters.getHeaders)
                    .then(response => {
                        let file = response.data
                        file.id = id
                        console.log(file)
                        commit('addEventFile', response.data)
                        dispatch('getFilesUrls')
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
        async getEventsFilesIds({state, rootState, rootGetters}){
            const path = `${state.defaultRoot}/${state.event.id}/files`
            let filesIds;
            await instance
                .get(path, rootGetters.getHeaders)
                .then(response => {
                    console.log(response.data)
                    filesIds = response.data
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
            return filesIds;
        },
        async getFilesUrls({state, commit}){
            commit('clearUrls')
            await state.files.forEach(file => {
                const url = `data:${file.contentType};base64,${file.content}`
                commit('addFileUrl', url)
            })
        },
        async removeFile({state, commit,dispatch, rootState, rootGetters}, data){
            const file = await dispatch('findFileByData', data)
            const path = `${state.defaultRoot}/${state.event.id}/files/${file.id}/delete`
            await instance
                .delete(path, rootGetters.getHeaders)
                .then(response => {
                    console.log(response)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
                .then(() => {
                    commit('setEventFiles', state.files.filter(f => f.id !== file.id ))
                    dispatch('getFilesUrls')
                    console.log(state.urls)
                })
        },
        async findFileByData({state}, data){
            return state.files.filter(file => file.content.slice(-32) === data)[0]
        }
    },
    namespaced: true

}