import axios from "./Interceptor";

export function getLeaderBoard(){
    return axios.get('http://localhost:5193/api/LeaderBoard?pageNumber=1&pageSize=5') 
}

export function sortLeaderBoard(choice,id,order){
    return axios.get(`http://localhost:5193/api/LeaderBoard/sort/${id}?choice=${choice}&order=${order}`)
}