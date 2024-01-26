import styled from "styled-components";
import { colors } from "../enums";
import PageLayout from "./PageLayout";

type ratingList = {
  place: number;
  login: string;
  rating: number;
}[];

const ListContainer = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0 8px;
  gap: 4px;
  width: 100%;
`

const UserContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 8px;
  border-bottom: 1px solid ${colors.black};
  width: 100%;
`

const list : ratingList = [
  {
    place: 1,
    login: 'Чел 3',
    rating: 66
  },
  {
    place: 2,
    login: 'Чел 2',
    rating: 12
  },
  {
    place: 3,
    login: 'Чел 1',
    rating: 4
  },
  {
    place: 4,
    login: 'Чел 4',
    rating: 1
  },
]

const RaitingList : React.FC = () => {
  // const [currentPage, setCurrentPage] = useState(1);

  return (
    <PageLayout>
      <ListContainer>
        {list.map((user) =>
          (
            <UserContainer>
              <p>{`${user.place}. ${user.login}`}</p>
              <p>{user.rating}</p>
            </UserContainer>
          )
        )}
      </ListContainer>
    </PageLayout>
  )
}

export default RaitingList;