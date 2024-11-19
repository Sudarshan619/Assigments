import axios from "./Interceptor";

export function getLeaderBoard(){
    return axios.get('http://localhost:5193/api/LeaderBoard/1') 
}