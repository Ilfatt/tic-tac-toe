import { useState } from "react";
import InputComponent from "./InputComponent";
import PageLayout from "./PageLayout";
import styled from "styled-components";
import { colors } from "../enums";
import UseStores from "../hooks/useStores";
import { useNavigate } from "react-router-dom";

const Button = styled.button`
  padding: 14px 48px;
  background-color: ${colors.blue};
  color: ${colors.white};
  border: none;
  border-radius: 10px;
  font-size: 16px;
  cursor: pointer;
`

const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 24px;
  justify-content: center;
  align-items: center;
`

const CreateGameModal: React.FC = () => {
  const [maxRating, setMaxRating] = useState<number>(0);
  const { lobbyListPageStore } = UseStores();
  const navigate = useNavigate();

  return (
    <PageLayout>
      <Container>
        <InputComponent
          value={maxRating.toString()}
          onChangeValue={(val) => {
            setMaxRating(Number(val))
          }}
          placeholder='Введите максимальный рейтинг противника'
          textAlign="center"
          validate="number"
          width="400"
          min={0}
        />
        <Button
          onClick={() => {
            const id = lobbyListPageStore.createLobby(maxRating)
            navigate(`lobby/${id}`)
          }}
        >
          Создать
        </Button>
      </Container>
    </PageLayout>
  )
}

export default CreateGameModal;