import {instance} from "@/router/instance";
import router from "@/router/router";


export const fileModule = {
    state: () => ({
        fileTypes: [],
        file: {
            fileType: '',
            fileSize: 1
        },
        file_ids: [],
        imageBlobs: [],
        textBlobs: [],
        isLoading: false,
        defaultRoot: 'file-settings/types',
        defaultEventRoot: 'events'
    }),

    mutations: {
        setDefaultRoot(state, defaultRoot){
            state.defaultRoot = defaultRoot
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
        },
        setLoading(state, bool){
            state.isLoading = bool
        },
        setAllowedFileTypes(state, types){
            state.fileTypes = types
        },
        addAllowedFileType(state, type){
            state.fileTypes.push(type)
        },
        setFileType(state, fileType){
            state.file = fileType
        },
        clearFileType(state){
            state.file = []
        }
    },
    actions: {
        async getEventFiles({state, commit, dispatch, rootState, rootGetters}, event_id){
            await commit('clearBlobs')
            await commit('setLoading', true)
            await dispatch('getEventsFilesIds', event_id)

            await state.file_ids.forEach(id => {
                const path = `${state.defaultEventRoot}/${event_id}/files/${id}`
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
        async getEventsFilesIds({state, commit, rootState, rootGetters}, event_id){
            const path = `${state.defaultEventRoot}/${event_id}/files/ids`
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
        async removeFile({state, dispatch, rootState, rootGetters}, ids){
            const event_id = ids[0]
            const file_id = ids[1]
            const path = `${state.defaultEventRoot}/${event_id}/files/${file_id}/delete`
            await instance
                .delete(path, {headers: rootGetters.getHeaders})
                .then(response => {
                    console.log(response)
                    dispatch('setBlobs',
                        [...state.imageBlobs, ...state.textBlobs]
                            .filter(blob => blob.id !== file_id )
                    )
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
        },
        async setBlobs({commit}, blobs){
            commit('clearBlobs')
            blobs.forEach(blob => commit('addBlob', blob))
        },
        async uploadFiles({commit, dispatch, rootState}, event_id){
            await commit('setLoading', true)
            const form = new FormData(document.querySelector('#uploadForm'))
            var data = document.querySelector('#files')

            for(let i = 0; i < data.files.length; i++)
                form.append('files', data.files[i])
            console.log(form)

            const path = `events/${event_id}/files/add-files`

            await instance
                .post(path, form, {
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        Authorization: `Bearer ${rootState.accessToken}`,
                    }})
                .then(() => {
                    dispatch('getEventFiles', event_id)
                    rootState.errors = []
                    console.log('ok')
                })
                .catch(error =>
                {
                    rootState.errors.push(error)
                    console.log(error)
                })
            await commit('setLoading', false)
        },
        async getAllowedFileTypes({state, commit, rootState, rootGetters}){
            const path = `${state.defaultRoot}`
            await commit('setAllowedFileTypes', [])
            await instance.get(path, {headers: rootGetters.getHeaders})
                .then(response => {
                    console.log(response.data)
                    commit('setAllowedFileTypes', response.data.allowedFileTypes)
                    console.log(state.fileTypes)
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
                })
        },
        async addFileType({state, commit, rootState, rootGetters}){
            const path = `${state.defaultRoot}/add`
            console.log(state.file)
            await instance
                .post(path, {
                    fileType: state.file.fileType,
                    fileSize: state.file.fileSize
                }, {headers: rootGetters.getHeaders})
                .then(() => {
                   commit('addAllowedFileType', state.file)
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
                })


        },
        async updateFileType({state, rootState, rootGetters}){
            const path = `${state.defaultRoot}/${state.file.id}/update`
            await instance
                .put(path, {
                    fileType: state.file.fileType,
                    fileSize: state.file.fileSize
                }, {headers: rootGetters.getHeaders})
                .then(() => console.log('ok'))
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
                })

        },
        async getFileType({state}, id){
            return state.fileTypes.filter(t => t.id === id)[0]
        },
        async removeFileType({state, commit, rootState, rootGetters}, id){
            const path = `${state.defaultRoot}/${id}/delete`
            await instance.delete(path, {headers: rootGetters.getHeaders})
                .then(() => {
                    commit('setAllowedFileTypes',
                        state.fileTypes
                            .filter(t => t.id !== id )
                    )
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
                })

        }
    },
    namespaced: true

}