import { createRouter, createWebHistory } from 'vue-router'
import List from '@/views/List.vue'
import Coins from '@/views/Coins.vue'
import AdminMenu from '@/components/AdminMenu.vue'
import VendingMachine from '@/components/VendingMachine.vue'

const routes = [
    {
        path: '/',
        component: VendingMachine
    },
    {
        path: '/:vendingname',
        component: List
    },
    {
        path: '/admin/:scode',
        component: AdminMenu
    },
    {
        path: '/admin/:scode/Coins',
        component: Coins
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
