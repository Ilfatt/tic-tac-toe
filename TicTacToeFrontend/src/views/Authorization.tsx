import { Route, Routes, useNavigate } from "react-router-dom"
import { useCallback, useEffect, useState } from "react"
import LogIn from "../components/Authorization/LogIn"
import Register from "../components/Authorization/Register"
import PageLayout from "../components/PageLayout"
import { icons } from "../enums"
import styled from "styled-components"
import UseStores from "../hooks/useStores"
import { runInAction } from "mobx"

const Logo = styled.img`
  margin: 100px 0 100px;
  max-width: 150px;
  max-height: 150px;
`

const Authorization = () => {
  const navigate = useNavigate();
  const { userStore } = UseStores();
  const [error, setError] = useState<string>();
  const [userName, setUserName] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [repeatPassword, setRepeatPassword] = useState<string>('');

  useEffect(() => {
    navigate('/authorization/login')
  }, [])

  const onLogInHandler = useCallback(async () => {
    await userStore.LogIn(userName, password)
    if (userStore.state?.isSuccess) {
      navigate('/')
    } else {
      runInAction(() => {
        setError(userStore.state?.error)
      })
    }
  }, [userName, password])

  const onRegistrationHandler = useCallback(async () => {
    if (password === repeatPassword) {
      await userStore.Registration(userName, password)
      if (userStore.state?.isSuccess) {
        navigate('/')
      } else {
        runInAction(() => {
          setError(userStore.state?.error)
        })
      }
    } else {
      setError('Пароли не совпадают')
    }
  }, [userName, password, repeatPassword])

  return (
    <PageLayout>
      <Logo src={icons.logo} alt='XO' />
      <Routes>
        <Route
          path="/login"
          element={
            <LogIn
              setUserName={setUserName}
              setPassword={setPassword}
              onClick={onLogInHandler}
              error={error}
            />
          }
        />
        <Route
          path="/registration"
          element={
            <Register
              setUserName={setUserName}
              setPassword={setPassword}
              setRepeatPassword={setRepeatPassword}
              onClick={onRegistrationHandler}
              error={error}
            />
          }
        />
      </Routes>
    </PageLayout>
  )
}

export default Authorization