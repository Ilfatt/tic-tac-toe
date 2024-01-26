import React, { useEffect, useState } from "react";
import styled from "styled-components";
import {colors} from "../enums"
import { useNavigate } from "react-router-dom";

const CardContainer = styled.div`
  padding: 0 16px;
  box-sizing: border-box;
  max-width: 600px;
  max-height: 200px;
  width: 100%;
  margin: 8px;
  border: 1px ${colors.gray} solid;
  border-radius: 24px;
  display: flex;
  flex-direction: column;
`;

const CardHeader = styled.div`
  display: flex;
  font-size: 18px;
  padding: 12px;
  font-weight: 600;
  align-items: center;
  justify-content: space-between;
`;

const CardBody = styled.div`
  display: flex;
  align-items: left;
  justify-content: center;
  flex-direction: column;
  font-size: 14px;
  padding: 8px;
  gap: 8px;
`

const ButtonContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
`

const JoinButton = styled.div`
  width: 100%;
  padding: 12px;
  margin: 8px 0px;
  border-radius: 12px;
  background-color: ${colors.blue};
  color: ${colors.white};
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
`

interface Props {
    lobbyOwner: string;
    lobbyOwnerRating: number;
    lobbyRating: number;
    lobbyId: string;
    statusProps: string;
}

const LobbyCard : React.FC<Props> = ({
  lobbyOwner, lobbyRating, statusProps, lobbyId, lobbyOwnerRating
}) => {
  const navigate = useNavigate();
  const [status, setStatus] = useState(statusProps);

  useEffect(() => {
    if (statusProps != status) {
      setStatus(statusProps);
    }
  }, [statusProps])

  return (
    <CardContainer>
      <CardHeader>
        <p>Лобби игрока {lobbyOwner}</p>
        <p>{status}</p>
      </CardHeader>
      <CardBody>
        <p>Максимальный рейтинг: {lobbyRating}</p>
        <p>Рейтинг создателя: {lobbyOwnerRating}</p>
      </CardBody>
      <ButtonContainer>
        <JoinButton
          onClick={() => {
            navigate(`lobby/${lobbyId}`)
            }
          }
        >
          Зайти в лобби
        </JoinButton>
      </ButtonContainer>
    </CardContainer>
  )
}

export default LobbyCard;