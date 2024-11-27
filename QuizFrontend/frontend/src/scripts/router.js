import LoginComponent from "@/components/LoginComponent.vue";
import QuestionsComponent from "@/components/QuestionsComponent.vue";
import HelloWorld from "@/components/HelloWorld.vue";
import { createRouter,createWebHistory } from "vue-router";
import QuizComponent from "@/components/QuizComponent.vue";
import QuizStartComponent from "@/components/QuizStartComponent.vue";
import RegisterComponent from "@/components/RegisterComponent.vue";
import ProfileComponent from "@/components/ProfileComponent.vue";
import LeaderBoardComponent from "@/components/LeaderBoardComponent.vue";
import UserProfile from "@/components/UserProfile.vue";
import RedirectedComponent from "@/components/RedirectedComponent.vue";

const routes=[
    {path: '/', component: HelloWorld},
    {path: '/login', component: LoginComponent},
    {path: '/questions', component: QuestionsComponent},
    {path: '/quiz', component: QuizComponent},
    {path:'/register',component:RegisterComponent},
    {path:'/profile',component:ProfileComponent},
    {path:'/userprofile',component:UserProfile},
    {path:'/leaderboard',component:LeaderBoardComponent},
    {path: '/takequiz/:quizId',
        name:'takequiz',
        component: QuizStartComponent
    },
    {path:'/:pathMatch(.*)*',component:RedirectedComponent}
];

const router = createRouter({
    history: createWebHistory(),
    routes
})

router.beforeEach((to, from, next) => {
    const isAuthenticated = !!sessionStorage.getItem('Token'); 
    if (!isAuthenticated && to.path !== '/login' && to.path !== '/register') {
        next({ path: '/login' });
    } else {
        next();
    }
  });

export default router;