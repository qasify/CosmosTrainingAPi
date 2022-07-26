import axios from 'axios';
import React, { useEffect } from 'react'
import { useState } from 'react'
import { useNavigate } from 'react-router-dom'

export default function Profile() {
    const navigate = useNavigate();
    const [myPosts, setMyPosts] = useState([])
    const [form, setForm] = useState(false)
    const [oldPassword, setOldPassword] = useState("")
    const [newPassword, setNewPassword] = useState("")
    const [status, setStatus] = useState("")
    
    const updatePassword = (e) => {
        e.preventDefault();
        axios.patch("https://localhost:7279/api/User/UpdateUserPassword", {
            username: localStorage.getItem("user"),
            oldPassword: oldPassword,
            newPassword: newPassword
        })
    }
    
    useEffect(()=>{
        if (localStorage.getItem("session") !== "true") {
            navigate("/Login")      
        }
    }, [])
    return (
        <>
        {form === true ? <>
            <h1 className='subtitle mb-5'>Update Password</h1>
            <form className='is-flex is-flex-direction-column'>
                <div class="field">
                    <label class="label">Old Password</label>
                    <div class="control">
                        <input class="input is-success" type="text" placeholder="Password" value={oldPassword} onChange={(e) => { setOldPassword(e.target.value) }} />
                    </div>
                </div>
                <div class="field">
                    <label class="label">New Password</label>
                    <div class="control">
                        <input class="input is-success" type="password" placeholder="Password" value={newPassword} onChange={(e) => { setNewPassword(e.target.value) }} />
                    </div>
                </div>
                
                <div class="field is-grouped mt-4">
                    <div class="control">
                        <button class="button is-link" onClick={(e)=>{updatePassword(e)}}>Update</button>
                    </div>
                    <div class="control">
                        <button class="button is-link is-light" onClick={()=>{setForm(false)}}>Cancel</button>
                    </div>
                </div>
                <span>{status}</span>
            </form>
        </> : <div className='has-text-centered mt-5'>
        <h1 className='title'>{localStorage.getItem("user")}'s Profile</h1>
        <div className='is-flex is-flex-direction-row is-justify-content-space-between'>
            <div>
                <h1 className='subtitle'>{localStorage.getItem("user")}'s Posts</h1>
            </div>
            <div>
                <button className='button mr-2' onClick={()=>{navigate("/Home")}}>Home</button>
                <button className='button' onClick={()=>{setForm(true)}}>Update Profile</button>
            </div>
        </div>
        <div>
            {myPosts.length !== 0 ? <></> : <></>}
        </div>

    </div>}</>
        
        
    )
}
