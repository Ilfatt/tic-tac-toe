import { Routes, Route, useNavigate } from "react-router-dom";
import App from "./App";
import PageHeader from "./components/PageHeader";
import LobbyPage from "./views/LobbyPage";
import UseStores from "./hooks/useStores";
import { useEffect } from "react";
import { observer } from "mobx-react";

const Router = () => {
	const { userStore } = UseStores();
	const navigate = useNavigate();
	console.warn(userStore.username);

	useEffect(() => {
		if (!userStore.token) {
			navigate('/authorization')
		}
	}, [])

	return (
		<>
			<PageHeader />
			<Routes>
				<Route path='/' element={<App />}/>
				<Route path='lobby/:id' element={<LobbyPage />} />
				<Route path='*' element={<>ой, я забыл удалить это отсюда</>} />
			</Routes>
		</>
	)
}

export default observer(Router);