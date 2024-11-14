import LoginComponent from "@/components/LoginComponent.vue";
import QuestionsComponent from "@/components/QuestionsComponent.vue";
import HelloWorld from "@/components/HelloWorld.vue";
import { createRouter,createWebHistory } from "vue-router";
import QuizComponent from "@/components/QuizComponent.vue";
import QuizStartComponent from "@/components/QuizStartComponent.vue";
import RegisterComponent from "@/components/RegisterComponent.vue";

const routes=[
    {path: '/', component: HelloWorld},
    {path: '/login', component: LoginComponent},
    {path: '/questions', component: QuestionsComponent},
    {path: '/quiz', component: QuizComponent},
    {path:'/takequiz',component:QuizStartComponent},
    {path:'/register',component:RegisterComponent}
];

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;