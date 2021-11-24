import Home from "@/pages/Home";
import {createRouter, createWebHistory} from 'vue-router'
import MyEventPage from "@/pages/MyEventPage";
import EventPage from "@/pages/EventPage";


const routes = [
    {
        path: "/",
        component: Home
    },
    {
        path: "/my/events",
        component: MyEventPage
    },
    {
        path: "/events",
        component: EventPage
    }
]

const router = createRouter({
        routes,
        history: createWebHistory(process.env.BASE_URL)
    })


export default router