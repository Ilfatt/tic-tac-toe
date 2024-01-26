import PageLayout from "./PageLayout";
import styled from "styled-components";
import { colors } from "../enums";
import UseStores from "../hooks/useStores";
import { removeToken } from "../services/ApiConnection";

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
  width: 100%;
`

const UserData = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 100%;
  align-items: start;
  justify-content: center;
  gap: 12px;
  padding: 0 24px;
`

const ProfileModal: React.FC = () => {
  const { userStore } = UseStores();

  return (
    <PageLayout>
      <Container>
        <UserData>
          <h2>
            Ник: {userStore.username}
          </h2>
          <h3>
            Рейтинг: {userStore.rating}
          </h3>
        </UserData>

        <Button
          onClick={() => {
            removeToken();
            userStore.clearStore();
            window.location.reload();
          }}
        >
          Выйти
        </Button>
      </Container>
    </PageLayout>
  )
}

export default ProfileModal;