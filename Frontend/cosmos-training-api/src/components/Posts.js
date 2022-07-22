import React, { useEffect, useState} from 'react'
import axios from 'axios'

export default function Posts(props) {
    const [posts, setPosts] = useState([]);
    useEffect(()=>{
        axios.get("https://localhost:7279/api/Post/GetAllposts").then(r => {
            console.log(r);
            setPosts(r.data);
        })
    }, [props.refresh])
  return (
    <div>{posts.length === 0 ? <></> : posts.map(p => <tr>{p.username}-------- {p.text}</tr>)}</div>
  )
}
