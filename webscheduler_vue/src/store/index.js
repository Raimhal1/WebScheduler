import {createStore} from 'vuex'
import {eventModule} from "./eventModule";
import {userModule} from "./userModule"



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
        }
    },
    getters: {
        getHeaders(state){
           return {
               headers: {
                   Authorization: `Bearer ${state.accessToken}`,
               }
           }
        }
    },

    modules: {
        event: eventModule,
        user: userModule,
    }
})