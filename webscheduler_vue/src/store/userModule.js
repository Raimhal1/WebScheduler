import jwt_decode from 'jwt-decode'
import router from "@/router/router";
import {instance} from "@/router/instance";

export const userModule = {
    state: () => ({
        user: {
            FirstName: null,
            LastName: null,
            UserName: null,
            Email: null,
            Password: null
        },
        invalid: false,
        defaultRoot: 'account',
        defaultUserRoot: 'users',
        isLoading: false
    }),
    getters: {},
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
        check(state, bool){
            state.invalid = bool
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
                    commit('check', false)
                })
                .catch(error => {
                    console.log(error)
                    rootState.errors = [...rootState.errors, error]
                    commit('check', true)
                })
                .then(() =>{
                    if(rootState.errors.length === 0)
                        router.push('/login')
                    else
                        router.push('/register')
                })
            await commit('setLoading', false)
        },
        async login({state, commit, dispatch,rootState}){
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
                    commit('check', false)
                    console.log('ok')
                })
                .catch(error => {
                    console.log(error.message)
                    rootState.errors = [...rootState.errors, error]
                    commit('check', true)
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
        async logout({rootState}){
            rootState.accessToken = ''
            rootState.refreshToken = ''
            rootState.isAuth = false
            rootState.isAdmin = false
            localStorage.accessToken = rootState.accessToken
            localStorage.refreshToken = rootState.refreshToken
            localStorage.isAuth = rootState.isAuth
            localStorage.isAdmin = rootState.isAdmin

        },
        async getUser({state, commit, rootState, rootGetters}, user_id){
            const path = `${state.defaultUserRoot}/${user_id}`
            return await instance
                .get(path, {headers: rootGetters.getHeaders})
                .then(res => {
                    res.data
                    rootState.errors = []
                    commit('check', false)
                })
                .catch(error =>{
                    console.log(error)
                    rootState.errors = [...rootState.errors, error]
                    commit('check', true)
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