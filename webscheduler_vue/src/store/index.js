import {createStore} from 'vuex'
import {eventModule} from "./eventModule";


export default createStore({
    state: {
        isAuth: false,
    },
    modules: {
        event: eventModule,
        my_event: eventModule
    }
})