import jwt_decode from 'jwt-decode'
import router from "@/router/router";
import {instance} from "@/router/instance";

export const userModule = {
    state: () => ({
        users: [],
        user: {
            FirstName: null,
            LastName: null,
            UserName: null,
            Email: null,
            Password: null
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
                FirstName: null,
                LastName: null,
                UserName: null,
                Email: null,
                Password: null
            }
        },
        setUser(state, user){
            state.user = user
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
                    response.data
                    rootState.errors = []
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors.push(error)
                })
                .then(() =>{
                    if(rootState.errors.length === 0)
                        router.push('/login')
                    else
                        router.push('/register')
                })
            await commit('setLoading', false)
        },
        async login({state, dispatch,rootState}){
            const path = `${state.defaultRoot}/authenticate`
            rootState.errors = []
            await instance
                .post(path, {
                    UserName: state.user.Email,
                    Password: state.user.Password
                })
                .then(response =>{
                    rootState.accessToken = response.data.jwtToken
                    rootState.refreshToken = response.data.refreshToken
                    rootState.isAuth = true
                    rootState.errors = []
                    console.log('ok')
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors.push(error)
                })
                .then(() => {
                    if (rootState.errors.length === 0) {
                        dispatch('decodeRoleFromJWT')
                        router.push('/')
                    }
                    else
                        router.push('/login')
                })
        },
        async logout({commit, rootState}){
            commit('clearUser')
            rootState.accessToken = ''
            rootState.refreshToken = ''
            rootState.isAuth = false
            rootState.isAdmin = false
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
                    rootState.errors = []
                })
                .catch(error =>{
                    console.log(error)
                    rootState.errors.push(error)
                })
        },
        async getUserByEmail({state, commit, rootState, rootGetters}, email){
            var data = new FormData();
            data.append('email', email)
            const path = `${state.defaultUserRoot}/email`

            await instance
                .get(path,
                    email,
                    {
                        "Content-Type": "multipart/form-data",
                        headers: rootGetters.getHeaders
                    })
                .then(response => {
                    commit('setUser', response.data)
                    rootState.errors = []
                })
                .catch(error =>{
                    console.log(error)
                    rootState.errors.push(error)
                })
        },
        async getUsers({state, commit, rootState, rootGetters}){
            await instance
                .get(state.defaultUserRoot, {headers: rootGetters.getHeaders})
                .then(response => {
                    console.log(response)
                    commit('setUsers', response.data)
                    rootState.errors = []
                })
                .catch(error =>{
                    console.log(error)
                    rootState.errors.push(error)
                })
        },
        async removeUser({state, commit, rootState, rootGetters}, user_id){
            const path = `${state.defaultUserRoot}/${user_id}/delete`
            await instance
                .delete(path, {headers: rootGetters.getHeaders})
                .then(() =>
                    rootState.errors = []
                )
                .catch(error =>{
                    console.log(error)
                    rootState.errors.push(error)
                })
                .then(() => {
                    commit('setUsers',
                        [...state.users]
                            .filter(user => user.id !== user_id )
                    )
                })
        },
        async decodeRoleFromJWT({rootState}){
            const payload = jwt_decode(rootState.accessToken)
            rootState.isAdmin = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin';
            console.log(rootState.isAdmin)
        }
    },
    namespaced: true

}