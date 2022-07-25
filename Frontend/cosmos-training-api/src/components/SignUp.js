import axios from 'axios'
import React from 'react'
import { useState} from 'react'
import { useNavigate } from "react-router-dom";

export default function SignUp() {
    const navigate = useNavigate();
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [gender, setGender] = useState("")

    const postData = (e) => {
        e.preventDefault();
        axios.post("https://localhost:7279/api/User", {
            "username":username,
            "password":password,
            "gender":gender
        }).then(r => {
            console.log(r);
            navigate("/")
        })
        
    }

    return (
        <div>
            <h1 className='title mb-5'>Cosmos Database Training API</h1>
            <h1 className='subtitle mb-5'>Sign Up</h1>
            <form className='is-flex is-flex-direction-column'>
                <div class="field">
                    <label class="label">Username</label>
                    <div class="control">
                        <input class="input is-success" type="text" placeholder="Username" value={username} onChange={(e) => { setUsername(e.target.value) }} />
                    </div>
                </div>
                <div class="field">
                    <label class="label">Password</label>
                    <div class="control">
                        <input class="input is-success" type="password" placeholder="Password" value={password} onChange={(e) => { setPassword(e.target.value) }} />
                    </div>
                </div>
                <div class="field">
                    <label class="label">Gender</label>
                    <div class="control">
                        <div class="select">
                            <select onChange={(e)=>{setGender(e.target.value)}}>
                                <option>N/A</option>
                                <option>Male</option>
                                <option>Female</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="field is-grouped mt-4">
                    <div class="control">
                        <button class="button is-link" onClick={postData}>Submit</button>
                    </div>
                    <div class="control">
                        <button class="button is-link is-light" onClick={() => {
                            navigate("/")
                        }}>Login</button>
                    </div>
                </div>
            </form>
        </div>
    )
}
