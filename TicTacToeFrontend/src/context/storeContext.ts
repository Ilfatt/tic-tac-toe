import { createContext } from "react";
import UserStore from "../stores/userStore";

export const storeContext = createContext({
    userStore: new UserStore(),
  });