import { Routes, Route } from "react-router-dom";
import App from "./App";
import PageHeader from "./components/PageHeader";
import LobbyPage from "./views/LobbyPage";

const Router = () => {

	return (
		<>
			<PageHeader />
			<Routes>
				<Route path='/' element={<App />}/>
				<Route path='film/:id' element={<LobbyPage />} />
				<Route path='*' element={<>ой, я забыл удалить это отсюда</>} />
			</Routes>
		</>
	)
}

export default Router;