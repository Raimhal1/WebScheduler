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

    modules: {
        event: eventModule,
        user: userModule,
        file: fileModule
    }
})