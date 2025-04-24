import { createContext, useContext, useState, useEffect } from "react";
import API from "../services/api";

const UserContext = createContext();

export const useUser = () => useContext(UserContext);

export const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null); 

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const res = await API.get("/auth/me");
        if (res.statusText === 'OK') {
          setUser(res?.data?.userId);
        }
      } catch (err) {
        console.error("Session check failed:", err);
      }
    };

    fetchUser();
  }, []);

  return (
    <UserContext.Provider value={{ user, setUser }}>
      {children}
    </UserContext.Provider>
  );
};
