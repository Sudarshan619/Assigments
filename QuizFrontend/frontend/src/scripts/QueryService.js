import axios from "axios";

export function getAllQuery(){
    return axios.get("http://localhost:5193/api/Query")
}

export function deleteQuery(id){
    return axios.delete(`http://localhost:5193/api/Query/${id}`)
}

export function updateQuery(id,reportedBy,queryType,description){
    return axios.put(`http://localhost:5193/api/Query/${id}`,{
        "reportedBy": reportedBy,
        "queryType": queryType,
        "description": description,
        "isResolved": true
    })
}