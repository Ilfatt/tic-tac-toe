import { createContext } from "react";
import UserStore from "../stores/userStore";
import LobbyPageStore from "../stores/lobbyPageStore";
import LobbyListPageStore from "../stores/lobbyListPageStore";


export const storeContext = createContext({
    userStore: new UserStore(),
    lobbyPageStore: new LobbyPageStore(),
    lobbyListPageStore: new LobbyListPageStore(),
  });