import React from "react";
import { useParams } from "react-router-dom";
import PageLayout from "../components/PageLayout";

const LobbyPage: React.FC = () => {
  const { id } = useParams();
  console.warn(id);
  //Тут будет вебсокет

  return (
    <PageLayout>
      <>dasdad</>
    </PageLayout>
  )
}

export default LobbyPage;