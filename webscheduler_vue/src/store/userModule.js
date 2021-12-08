import jwt_decode from 'jwt-decode'
import router from "@/router/router";
import {instance} from "@/router/instance";

export const userModule = {
    state: () => ({
        users: [],
        user: {
            firstName: "",
            lastName: "",
            userName: "",
            email: "",
            password: ""
        },
        defaultRoot: 'account',
        defaultUserRoot: 'users',
        isLoading: false
    }),
    getters: {},
    mutations: {
        setUsers(state, users){
            state.users = users
        },
        clearUser(state){
            state.user = {
                firstName: "",
                lastName: "",
                userName: "",
                email: "",
                password: ""
            }
        },
        setUser(state, user){
            state.user = user
        },
        pushUser(state, user){
            state.users.push(user)
        },
        setDefaultRoot(state, defaultRoot) {
            state.defaultRoot = defaultRoot
        },
        setDefaultUserRoot(state, defaultUserRoot) {
            state.defaultUserRoot = defaultUserRoot
        },
        setLoading(state, bool) {
            state.isLoading = bool
        }
    },
    actions: {
        async register({state, commit, rootState}){
            await commit('setLoading', true)
            rootState.errors = []
            await instance
                .post(state.defaultUserRoot, state.user)
                .then(response => {
                    state.user.id = response.data
                    commit('pushUser', state.user)
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
                .then(() =>{
                    if(rootState.errors.length === 0)
                        router.push('/login')
                })
            await commit('setLoading', false)
        },
        async login({state, dispatch,rootState}){
            const path = `${state.defaultRoot}/authenticate`
            rootState.errors = []
            await instance
                .post(path, {
                    UserName: state.user.email,
                    Password: state.user.password
                })
                .then(response =>{
                    rootState.accessToken = response.data.jwtToken
                    rootState.refreshToken = response.data.refreshToken
                    rootState.isAuth = true
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push("Incorrect email or password")
                })
                .then(() => {
                    if (rootState.errors.length === 0) {
                        dispatch('decodeRoleFromJWT')
                        router.push('/')
                    }
                })
        },
        async logout({commit, rootState}){
            commit('clearUser')
            rootState.accessToken = ''
            rootState.refreshToken = ''
            rootState.isAuth = false
            rootState.isAdmin = false
            rootState.errors = []
            localStorage.accessToken = rootState.accessToken
            localStorage.refreshToken = rootState.refreshToken
            localStorage.isAuth = rootState.isAuth
            localStorage.isAdmin = rootState.isAdmin

        },
        async getUserById({state, commit, rootState, rootGetters}, user_id){
            const path = `${state.defaultUserRoot}/${user_id}`
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(response => {
                    commit('setUser', response.data)
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async getUserByEmail({state, rootState, rootGetters}, email){
            const path = `${state.defaultUserRoot}/${email}/email`
            return await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(response => response.data)
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async getUsers({state, commit, rootState, rootGetters}){
            rootState.errors = []
            await instance
                .get(state.defaultUserRoot, {headers: rootGetters.getHeaders})
                .then(response => {
                    commit('setUsers', response.data)
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async removeUser({state, commit, rootState, rootGetters}, user_id){
            const path = `${state.defaultUserRoot}/${user_id}/delete`
            await instance
                .delete(path, {headers: rootGetters.getHeaders})
                .then(() =>
                    commit('setUsers',
                        [...state.users]
                            .filter(user => user.id !== user_id )
                    ),
                )
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async decodeRoleFromJWT({rootState}){
            const payload = jwt_decode(rootState.accessToken)
            rootState.isAdmin = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin';
        },
        async GetCurrentUser({state, commit, rootState, rootGetters}){
            const path = `${state.defaultUserRoot}/current`
            await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(response => commit('setUser', response.data))
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        },
        async updateUser({state, rootState, rootGetters}){
            const path = `${state.defaultUserRoot}/${state.user.id}/update`
            rootState.errors = []
            await instance
                .put(path, state.user, {headers: rootGetters.getHeaders})
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error.response.data.error)
                })
        }
    },
    namespaced: true

}