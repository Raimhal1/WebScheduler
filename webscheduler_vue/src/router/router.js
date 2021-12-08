import {createRouter, createWebHistory} from 'vue-router'
import Home from "@/pages/Home";
import MyEventsPage from "@/pages/MyEventsPage";
import EventsPage from "@/pages/EventsPage";
import EventPage from "@/pages/EventPage";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";
import AdminPage from "@/pages/AdminPage";
import AccountPage from "@/pages/AccountPage";

const routes = [
    {
        path: "/",
        component: Home
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
    },
    {
        path: "/admin",
        component: AdminPage
    },
    {
        path: "/account",
        component: AccountPage
    }
]

const router = createRouter({
    routes,
    history: createWebHistory(process.env.BASE_URL)
})




export default router