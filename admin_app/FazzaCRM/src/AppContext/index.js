import axios from "axios";
import React,{useState} from "react";
import { createContext, useContext, useEffect, useMemo, useState } from "react";

const AppContext = createContext();

const AppContextProvider =({children})=>{
    const [token, setToken_] = useState(localStorage.getItem("token"));
    const [authorize, setauthorize] = useState(false)
  // Function to set the authentication token
  const setToken = (newToken) => {
    setToken_(newToken);
  };
  useEffect(() => {
    if (token) {
      setauthorize(true)
      axios.defaults.headers.Authorization= "Bearer " + token;
      localStorage.setItem("token", token);
    } else {
      delete axios.defaults.headers.Authorization;
      localStorage.removeItem("token");
      setauthorize(false)
    }
  }, [token]);

  const contextValue = useMemo(
    () => ({
      token,
      setToken,
      authorize,
      setauthorize
    }),
    [token]
  );
  return (
    <AppContext.Provider value={contextValue}>{children}</AppContext.Provider>
  );
};

export const useAppContext =()=>{
    return useContext(AppContext);
}

export default AppContextProvider;