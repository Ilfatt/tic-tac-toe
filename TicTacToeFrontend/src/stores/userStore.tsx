import { makeAutoObservable, runInAction } from "mobx";
import StateStore from "./StateStores/StateStore";
import FetchingStateStore from "./StateStores/FetchingStateStore";
import AuthorizationService from "../services/AuthorizationService";
import SuccessStateStore from "./StateStores/SuccessStateStore";
import ErrorStateStore from "./StateStores/ErrorStateStore";

class UserStore {
  token?: string;

  username?: string

  rating?: number;

  state?: StateStore

  constructor() {
    makeAutoObservable(this);
    this.token = localStorage.getItem('PortalToken') ?? undefined;
  }

  public async LogIn(login: string, password: string) {
    this.state = new FetchingStateStore();
    try {
      const response = await AuthorizationService.LogIn(login, password);
      localStorage.setItem('PortalToken', response.jwtToken);
      runInAction(() => {
        this.token = response.jwtToken;
        this.state = new SuccessStateStore();
      })
    }
    catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      })
    }
  }

  public async Registration(login: string, password: string) {
    this.state = new FetchingStateStore();
    try {
      const response = await AuthorizationService.Registration(login, password);
      localStorage.setItem('PortalToken', response.jwtToken);
      runInAction(() => {
        this.token = response.jwtToken;
        this.state = new SuccessStateStore();
      })
    }
    catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      })
    }
  }

  public async GetUserData() {
    this.state = new FetchingStateStore();
    try {
      const response = await AuthorizationService.GetUserData();
      runInAction(() => {
        this.username = response.username;
        this.rating = response.userRating;
        this.state = new SuccessStateStore();
      })
    }
    catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      })
    }
  }

  public clearStore() {
    this.token = undefined;
    this.username= undefined;
    this.rating= undefined;
    this.state= new StateStore();
  }
}

export default UserStore;