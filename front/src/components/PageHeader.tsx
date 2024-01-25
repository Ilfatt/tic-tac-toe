import styled from "styled-components"
import { useNavigate } from "react-router-dom"
import { icons } from "../enums";
import { colors } from "../enums"
import { useState } from "react";
import ModalSearchWindow from "./ModalWindow";
import RaitingList from "./RaitingList";

type ButtonProps = {
  isActive?: boolean | undefined
}

const Header = styled.div`
  display: flex;
  width: 100%;
  height: 70px;
  background-color: ${colors.lightGray};
  align-items: center;
  justify-content: space-between;
`;

const Icon = styled.button`
  border: none;
  background-color: ${colors.lightGray};
  width: 50px;
  height: 50px;
  cursor: pointer;
`

const ButtonContainer = styled.div`
  margin: 0 32px 0 32px;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 26px;
`

const Button = styled.div<ButtonProps>`
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
  padding: 8px 12px;
  text-decoration: none;
  font-weight: 500;
  font-size: 18px;
  line-height: 21.48px;
  color: ${(props) => props.isActive ? colors.white : colors.black};
  border-radius: 24px;
  background-color: ${(props) => props.isActive ? colors.blue : 'none'};
`

const PageHeader : React.FC = () => {
  const navigate = useNavigate();
  const [ratingModal, setRatingModal] = useState<boolean | undefined>(undefined);

  return (
    <Header>
      <ButtonContainer >
        <Button
          isActive={ratingModal}
          onClick={() => setRatingModal(true)}
        >
          Рейтинг
        </Button>
      </ButtonContainer>
      <Icon
        onClick={() => {navigate('/')}}
      >
        <img src={icons.logo}/>
      </Icon>
      <ButtonContainer>
        <Button>
          Выйти
        </Button>
      </ButtonContainer>

      {
        ratingModal && (
          <ModalSearchWindow
            onClose={() => {
              setRatingModal(undefined)
            }}
            title='Рейтинг'
            children={(
              <RaitingList />
            )}
            width={800}
          />
        )
      }
    </Header>
  )
}

export default PageHeader;