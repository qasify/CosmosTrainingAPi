import React, { useEffect, useState } from 'react'
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import Posts from './Posts';

const NavBar = () => {
    const navigate = useNavigate()
    return (
        <nav class="navbar" role="navigation" aria-label="main navigation">
            <div id="navbarBasicExample" class="navbar-menu">
                <div class="navbar-end">
                    <div class="navbar-item">
                        <div class="buttons">
                            <a class="button is-primary">
                                <strong>Profile</strong>
                            </a>
                            <a class="button is-light" onClick={()=>{
                                localStorage.removeItem("session");
                                localStorage.removeItem("user");
                                navigate("/")
                            }}>
                                Log Out
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </nav>
    )
}

export default function Home() {
    const [myPost, setMyPost] = useState([])
    const navigate = useNavigate()
    const [refresh, setRefresh] = useState(0)
    
    const postPost = () => {
        axios.post("https://localhost:7279/api/Post/addPost", {
            id: "",
            text: myPost,
            username: localStorage.getItem("user")
        }).then(r => {
          setRefresh(1);  
        })
    }
    
    useEffect(() => {
        if (localStorage.getItem("session") !== "true") {
            navigate("/Login")
        }
    }, [])
    
    return (
        <div>
            <NavBar className="mb-4"/>
            Welcome, {localStorage.getItem("user")}
            <div className='mt-4'>
                <div class="field">
                    <div class="control">
                        <textarea class="textarea is-large" placeholder="What's on your mind?" value={myPost} onChange={(e)=>{setMyPost(e.target.value)}}></textarea>
                    </div>
                </div>
                <button className='button' onClick={postPost}>Post</button>
            </div>
            <div>
                <Posts refresh={refresh}/>
            </div>
            <div style={{display: "None"}}>
                {refresh}
            </div>
        </div>
    )
}
