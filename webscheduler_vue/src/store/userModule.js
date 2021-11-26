import {instance} from '@/instance'

export const userModule = {
    state: () => ({
        user: {
            FirstName: null,
            LastName: null,
            UserName: null,
            Email: null,
            Password: null
        },
        accessToken: null,
        refreshToken: null,
        defaultRoot: 'account',
        defaultUserRoot: 'users',

    }),
    getters: {
        // getAccessHeaders({state}){
        //     return {
        //         headers: {
        //             Authorization: `Bearer ${state.user.accessToken}`
        //         }
        //     }
        // }
    },
    mutations: {
        setUser(state, user){
            state.user = user
        },
        setDefaultRoot(state, defaultRoot) {
            state.defaultRoot = defaultRoot
        },
        setDefaultUserRoot(state, defaultUserRoot) {
            state.defaultUserRoot = defaultUserRoot
        },
        updateStorage(state, {access, refresh}){
            state.accessToken = access
            state.refreshToken = refresh
        }
    },
    actions: {
        async register({state, commit, dispatch}){
            try {
                const result = await instance.post(state.defaultUserRoot, state.user)
                const user_id = result.data
                const user = await dispatch('getUser', user_id)
                console.log(user)
                commit('setUser', user)
            }
            catch (ex){
                console.log(ex.message)
            }
        },
        async login({state, commit}){
            const path = `${state.defaultRoot}/authenticate`
            console.log(path)
            await instance.post(path, {
                UserName: state.user.Email,
                Password: state.user.Password
            }).then(response =>{
                console.log(response)
                console.log(response.data)
                commit('updateStorage', {
                    access: response.data.jwtToken,
                    refresh: response.data.refreshToken
                })
                state.user.Email = response.data.userLogin

            })
        },
        async getUser({state}, user_id){
            const path = `${state.defaultUserRoot}/${user_id}`
            return await instance.get(path).then(res => res.data)
        },
    },
    namespaced: true

}