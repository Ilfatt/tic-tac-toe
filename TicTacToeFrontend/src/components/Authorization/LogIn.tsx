import { NavLink } from "react-router-dom"
import styled from "styled-components"
import { colors } from "../../enums"
import InputComponent from "../InputComponent"

interface Props {
  setUserName: (value: string) => void;
  setPassword: (value: string) => void;
  onClick: () => void;
  error?: string;
}

const UnderTitle = styled.p`
  font-size: 15px;
  font-weight: 400;
  color: ${colors.gray};
`

const NavigationLink = styled(NavLink)`
  color: ${colors.blue};
  font-weight: 700;
  text-decoration: none;
  margin-left: 4px;
`

const Button = styled.button`
  cursor: pointer;
  padding: 14px 40px;
  border: 3px solid ${colors.blue};
  margin: 100px 0 100px;
  border-radius: 25px;
  color: ${colors.blue};
  font-weight: 700;
  background-color: ${colors.white};
`

const ErrorMessage = styled.p`
  margin-top: 17px;
  color: ${colors.red};
  font-size: 13px;
  font-weight: 400;
`

const LogIn : React.FC<Props> = ({setUserName, setPassword, onClick, error}) => {

  return (
    <>
      <InputComponent
        placeholder="Логин"
        validate="text"
        width="375"
        isRequired={true}
        textAlign="center"
        onChangeValue={(value) => {
          setUserName(value)
        }}
        isError={!!error}
      />
      <InputComponent
        placeholder="Пароль"
        validate="password"
        width="375"
        isRequired={true}
        textAlign="center"
        onChangeValue={(value) => {
          setPassword(value)
        }}
        isError={!!error}
      />
      {error && (
        <ErrorMessage>{error}</ErrorMessage>
      )}
      <Button
        onClick={onClick}
      >
        Войти
      </Button>
      <UnderTitle>
        Еще не зарегистрированы?
        <NavigationLink to='/authorization/registration'>Регистрация</NavigationLink>
      </UnderTitle>
    </>
  )
}

export default LogIn