import Home from "@/pages/Home";
import {createRouter, createWebHistory} from 'vue-router'
import MyEventsPage from "@/pages/MyEventsPage";
import EventsPage from "@/pages/EventsPage";
import About from "@/pages/About";
import EventPage from "@/pages/EventPage";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";
// import store from '@/store'

const routes = [
    {
        path: "/",
        component: Home
    },
    {
        path: "/about",
        component: About
    },
    {
        path: "/login",
        component: LoginPage
    },
    {
        path: "/register",
        component: RegisterPage
    },
    {
        path: "/logout",
    },
    {
        name: 'My events',
        path: "/my/events",
        component: MyEventsPage
    },
    {
        name: 'Events',
        path: "/events",
        component: EventsPage,
    },
    {
        path: "/events/:id",
        component: EventPage
    }
]

const router = createRouter({
    routes,
    history: createWebHistory(process.env.BASE_URL)
})


export default router