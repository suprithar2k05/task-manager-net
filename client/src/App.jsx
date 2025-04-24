import React, { useState, useEffect } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Signup";
import Dashboard from "./pages/Dashboard";
import Navbar from "./components/Navbar";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Profile from "./pages/Profile";
import API from "./services/api";

const PrivateRoute = ({ user, children }) => {
  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return children;
};

function App() {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const fetchUser = async () => {
    try {
      const res = await API.get("/auth/me");
      setUser(res.data.userId);
    } catch {
      setUser(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUser();
  }, []);

  if (loading) {
    return <div>Loading...</div>; // or null, or a spinner
  }

  return (
    <>
      <Navbar user={user} setUser={setUser}/>
      <main className="container mx-auto px-4 py-6">
        <Routes>
          <Route
            path="/"
            element={
              <PrivateRoute user={user}>
                <Dashboard />
              </PrivateRoute>
            }
          />
          <Route path="/login" element={<Login user={user}/>} />
          <Route path="/signup" element={<Register user={user}/>} />
          <Route
            path="/profile"
            element={
              <PrivateRoute user={user}>
                <Profile />
              </PrivateRoute>
            }
          />
        </Routes>
      </main>
      <ToastContainer position="top-right" autoClose={3000} />
    </>
  );
}

export default App;
