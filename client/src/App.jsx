import React, { useState, useEffect } from "react";
import { Routes, Route, Navigate, BrowserRouter } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Signup";
import Dashboard from "./pages/Dashboard";
import Navbar from "./components/Navbar";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Profile from "./pages/Profile";
import { UserProvider, useUser } from "./context/UserContext";

const PrivateRoute = ({ children }) => {
  const { user } = useUser();
  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return children;
};

function App() {
  return (
    <>
    <UserProvider>
    <BrowserRouter>
      <Navbar/>
      <main className="container mx-auto px-4 py-6">
        <Routes>
          <Route
            path="/"
            element={
              <PrivateRoute>
                <Dashboard />
              </PrivateRoute>
            }
          />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<Register/>} />
          <Route
            path="/profile"
            element={
              <PrivateRoute >
                <Profile />
              </PrivateRoute>
            }
          />
        </Routes>
      </main>
      <ToastContainer position="top-right" autoClose={3000} />
      </BrowserRouter>
    </UserProvider>
    </>
  );
}

export default App;
