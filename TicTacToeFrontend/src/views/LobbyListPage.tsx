import { useEffect, useMemo, useState } from "react";
import LobbyCard from "../components/LobbyCard";
import PageLayout from "../components/PageLayout";
import styled from "styled-components";
import { colors } from "../enums";
import ModalSearchWindow from "../components/ModalWindow";
import CreateGameModal from "../components/CreateGameModal";
import UseStores from "../hooks/useStores";
import { useEffectOnce } from "react-use";
import { observer } from "mobx-react";

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

const LobbyListPage : React.FC = () => {
  const [createGameModal, setCreateGameModal] = useState<boolean>(false);
  const { userStore, lobbyListPageStore } = UseStores();
  const [page, setPage] = useState(1);

  useEffectOnce(() => {
    if ((!userStore.userId) && userStore.token) {
      userStore.GetUserData();
    }
    lobbyListPageStore.fetchLobbys(page);
  })

  const handleScroll = () => {
    if ((window.innerHeight + document.documentElement.scrollTop + 1 >= document.documentElement.scrollHeight)
      && lobbyListPageStore.totalCount !== lobbyListPageStore.lobbys?.length)
    {
      setPage(prev => prev + 1);
    }
  }

  useEffect(() => {
    window.addEventListener("scroll", handleScroll)

    return () => {
      window.removeEventListener("scroll", handleScroll);
      lobbyListPageStore.clearStore();
    }
  }, [])

  const LobbyList = useMemo(() => {
    return lobbyListPageStore.lobbys?.length ? (
      lobbyListPageStore.lobbys.map((lobby) => (
        <LobbyCard
          key={lobby.gameId}
          lobbyId={lobby.gameId}
          lobbyRating={lobby.maxrating}
          statusProps={lobby.gameState}
        />
      ))
    ) : (
      <p>Нет активных игр</p>
    )
  }, [lobbyListPageStore.lobbys])

  useEffect(() => {
    lobbyListPageStore.fetchLobbys(page);
  }, [page])

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
      {LobbyList}
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

export default observer(LobbyListPage);

