import { createApp } from 'vue'
import App from './App.vue'
import 'bootstrap/dist/css/bootstrap.css'
import router from './scripts/router'
import { createPinia } from 'pinia'

const pinia = createPinia();


createApp(App).use(pinia).use(router).mount('#app')
