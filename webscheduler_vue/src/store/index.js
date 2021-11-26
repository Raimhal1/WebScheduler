import {createStore} from 'vuex'
import {eventModule} from "./eventModule";
import {userModule} from "./userModule"


export default createStore({
    state: {
        isAuth: false,
    },
    modules: {
        event: eventModule,
        my_event: eventModule,
        user: userModule,
    }
})