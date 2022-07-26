import React, { useEffect, useState } from 'react'
import axios from 'axios'

export default function Posts(props) {
  const [posts, setPosts] = useState([]);
  useEffect(() => {
    axios.get("https://localhost:7279/api/Post/GetAllposts", {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`
      }
    }).then(r => {
      console.log(r);
      setPosts(r.data);
    })
  }, [props.refresh])

  const deletePost = (p) => {
    console.log({
      id: p.id,
      username: p.username
    })
    axios.delete("https://localhost:7279/api/Post/DeletePost", {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`
      },
      data: {
        id: p.id,
        username: p.username
      }
    }).then(r => {
      alert("Deleted successfully!");
    }).catch(err => {
      alert("You can not delete another user's posts.")
    })
  }

  return (
    <div className='is-flex is-flex-direction-column is-flex-justify-content-center'>{posts.length === 0 ? <></> : posts.map(p => <tr>
      <div class="card mt-4">
        <div class="card-content is-flex is-flex-direction-column">
          <div class="content">
            <div className='has-text-weight-bold'>
              {p.username}
            </div>
            <div>
              {p.text}
            </div>
          </div>
          <div className='is-align-self-flex-end'>
            <button className='button has-background-danger has-text-white' onClick={() => {
              deletePost(p);
            }}>Delete</button>
          </div>
        </div>
      </div></tr>)}</div>
  )
}
