import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import SignUp from './components/SignUp';
import Login from './components/Login';
import Home from './components/Home';
import Profile from './components/Profile';

function App() {
  return (
    <div className="App container">
      <Router>
        <Routes>
          <Route path="/" element={<Login />}></Route>
          <Route path="/SignUp" element={<SignUp />}></Route>
          <Route path="/Home" element={<Home />}></Route>
          <Route path="/Profile" element={<Profile />}></Route>
          <Route path="/*" element={<Navigate to="/Home" />}></Route>
          {/* <Route path="/profile" element={<ProfilePage />}></Route>
          <Route path="/friends" element={<FriendsPage />}></Route>
          <Route path="/friends/:email" element={<UserFriends />}></Route>
          <Route path="/pending-requests" element={<PendingFriends />}></Route>
          <Route path="/profile/:email" element={<UserProfilePage />}></Route> */}
        </Routes>
      </Router>
    </div>
  );
}

export default App;
