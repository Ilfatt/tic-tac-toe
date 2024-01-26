import { useState } from "react";
import InputComponent from "./InputComponent";
import PageLayout from "./PageLayout";
import styled from "styled-components";
import { colors } from "../enums";

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
  const [maxRating, setMaxRating] = useState<string>('');

  return (
    <PageLayout>
      <Container>
        <InputComponent
          value={maxRating}
          onChangeValue={(val) => {
            setMaxRating(val)
          }}
          placeholder='Введите максимальный рейтинг противника'
          textAlign="center"
          width="400"
        />
        <Button
          onClick={() => {
            ///
          }}
        >
          Создать
        </Button>
      </Container>
    </PageLayout>
  )
}

export default CreateGameModal;