import { useEffect, useState } from "react";
import LobbyCard from "../components/LobbyCard";
import PageLayout from "../components/PageLayout";
import styled from "styled-components";
import { colors } from "../enums";
import ModalSearchWindow from "../components/ModalWindow";
import CreateGameModal from "../components/CreateGameModal";

type Props = {
  lobbyOwner: string;
  lobbyOwnerRating: number;
  lobbyRating: number;
  lobbyId: string;
  status: string;
}[];

const TitleContainer = styled.div`
  width: 700px;
  padding: 8px 0;
  margin-bottom: 16px;
  display: flex;
  align-items: center;
  justify-content: space-between;
`

const Title = styled.div`
  font-size: 36px;
  font-weight: 600;
`
const Button = styled.button`
  padding: 12px;
  background-color: ${colors.blue};
  color: ${colors.white};
  border: none;
  border-radius: 12px;
  font-size: 16px;
  cursor: pointer;
`

const LobbyExample : Props  = [
  {
    lobbyOwner: 'Чел2',
    lobbyRating: 123,
    lobbyOwnerRating: 50,
    lobbyId: '1',
    status: 'Ожидание'
  },
  {
    lobbyOwner: 'Чел',
    lobbyRating: 1234,
    lobbyOwnerRating: 5000,
    lobbyId: '2',
    status: 'Ожидание'
  },
  {
    lobbyOwner: 'Чел3',
    lobbyRating: 60,
    lobbyOwnerRating: 100,
    lobbyId: '3',
    status: 'Ожидание'
  },
]

const LobbyListPage : React.FC = () => {

  const [createGameModal, setCreateGameModal] = useState<boolean>(false);

  useEffect(() => {
    /** фетч всех лобби */
  }, [])

  return (
    <PageLayout>
      <>
      <TitleContainer>
        <Title>Текущие игры</Title>
        <Button
          onClick={() => {
            setCreateGameModal(true);
          }}
        >
          Создать игру
        </Button>
      </TitleContainer>
      {
        LobbyExample.map((lobby) => (
          <LobbyCard
            key={lobby.lobbyId}
            lobbyId={lobby.lobbyId}
            lobbyOwner={lobby.lobbyOwner}
            lobbyRating={lobby.lobbyRating}
            lobbyOwnerRating={lobby.lobbyOwnerRating}
            statusProps={lobby.status}
          />
        ))
      }
      {
        createGameModal && (
          <ModalSearchWindow
            title='Создать игру'
            onClose={() => setCreateGameModal(false)}
            children={<CreateGameModal />}
          />
        )
      }
      </>
    </PageLayout>
  )
}

export default LobbyListPage;

