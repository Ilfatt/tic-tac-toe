import React, { useMemo } from "react";
// import { useParams } from "react-router-dom";
import PageLayout from "../components/PageLayout";
import styled from "styled-components";
import { colors, icons } from "../enums";
import UseStores from "../hooks/useStores";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`

const GameContainer = styled.div`
  display: grid;
  grid-template-columns: repeat(3, 1fr); /* Создает 3 колонки с равной шириной */
  grid-template-rows: repeat(3, 1fr); /* Создает 3 строки с равной высотой */
  gap: 10px; /* Добавляет промежуток между элементами */
  width: 400px; /* Ширина квадратного элемента */
  height: 400px; /* Высота квадратного элемента */
`;

const GameBlock = styled.div`
  background-color: white;
  border: 2px solid ${colors.blue};
  display: flex;
  align-items: center;
  justify-content: center;
`;

const Icon = styled.img`
  width: 80px;
  height: 80px;
`

const PageBody = styled.div`
  display: flex;
  gap: 24px;
  background-color: #ccc;
  padding: 24px;
  border-radius: 12px;
`

const TitleContainer = styled.div`
  width: 700px;
  padding: 8px 0;
  margin-bottom: 16px;
  display: flex;
  align-items: center;
  justify-content: space-around;
  margin: 36px 0 ;
  font-size: 32px;
  font-weight: 600;
`

const ChatContainer = styled.div`
  display: flex;
  background-color: ${colors.gray};
  width: 250px;
  height: 400px;
  border-radius: 8px;
  padding: 8px;
`

const field = [0, 0, 1, 0, 0, 0, -1, 0, 0 ];

const LobbyPage: React.FC = () => {
  // const { id } = useParams();
  const { lobbyPageStore } = UseStores();
  //Тут будет вебсокет

  const fillGame = useMemo(() => {
    return field.map((el) => {
      if (el === 1) {
        return (
          <GameBlock>
            <Icon src={icons.cross} />
          </GameBlock>
        )
      } else if (el === -1) {
        return (
          <GameBlock>
            <Icon src={icons.circle} />
          </GameBlock>
        )
      } else {
        return (
          <GameBlock>
          </GameBlock>
        )
      }
    })
  }, [field])

  return (
    <PageLayout>
      <Container>
        <TitleContainer>
          <p>{lobbyPageStore.ownerName}</p>  <p>vs</p> <p>{lobbyPageStore.oponnetnName}</p>
        </TitleContainer>
        <PageBody>
          <GameContainer>
            {fillGame}
          </GameContainer>
          <ChatContainer>
            <>Заготовка под чат</>
          </ChatContainer>
        </PageBody>

      </Container>
    </PageLayout>
  )
}

export default LobbyPage;