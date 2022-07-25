import axios from 'axios'
import React from 'react'
import { useState } from 'react'
import { useNavigate } from "react-router-dom";

export default function Login() {
    const navigate = useNavigate();
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")

    const postData = (e) => {
        e.preventDefault();
        axios.post("https://localhost:7279/api/Authentication/AuthenciateUser", {
            "username":username,
            "password":password
        }).then(r => {
            if (r.data !== ""){
                localStorage.setItem("session", "true");
                localStorage.setItem("user", username);
                localStorage.setItem("token", r.data)
                navigate("/Home");
            }
        })
        
    }

    return (
        <div>
            <h1 className='title mb-5'>Cosmos Database Training API</h1>
            <h1 className='subtitle mb-5'>Login</h1>
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
                
                <div class="field is-grouped mt-4">
                    <div class="control">
                        <button class="button is-link" onClick={postData}>Login</button>
                    </div>
                    <div class="control">
                        <button class="button is-link is-light" onClick={() => {navigate("/SignUp")}}>Sign Up</button>
                    </div>
                </div>
            </form>
        </div>
    )
}
