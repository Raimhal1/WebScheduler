import {createStore} from 'vuex'
import {eventModule} from "./eventModule"
import {userModule} from "./userModule"
import {fileModule} from "./fileModule"

export default createStore({
    state: {
        isAuth: false,
        isAdmin: false,
        accessToken: '',
        refreshToken: '',
        errors: [],
        tokenExp: null,
    },
    mutations: {
        setTokens(state, {access, refresh}){
            state.accessToken = access
            state.refreshToken = refresh
        },
        setAuth(state, bool) {
            state.isAuth = bool
        },
        setAdmin(state, bool){
            state.isAdmin = bool
        },
        setExp(state, exp){
            state.tokenExp = exp
        },
        clearErrors(state){
            state.errors = []
        }
    },
    getters: {
        getHeaders(state){
           return {
               Authorization: `Bearer ${state.accessToken}`,
           }
        },

    },
    actions: {
        async logout({state}){
            state.accessToken = ''
            state.refreshToken = ''
            state.isAuth = false
            state.isAdmin = false
            state.tokenExp = null
            state.errors = []
            localStorage.accessToken = state.accessToken
            localStorage.refreshToken = state.refreshToken
            localStorage.isAuth = state.isAuth
            localStorage.isAdmin = state.isAdmin
            localStorage.tokenExp = state.tokenExp

        },
    },

    modules: {
        event: eventModule,
        user: userModule,
        file: fileModule
    }
})