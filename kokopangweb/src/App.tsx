import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Main from './routes/Main';
import Notice from './routes/Notice';
import GameGuide from './routes/GameGuide';
import Community from './routes/Community';
import Rank from './routes/Rank';
import SignUp from './routes/SignUp';
import CommunityDetail from './routes/CommunityDetail';
import CommunityCreate from './components/Community/CommunityCreate';
import CommunityUpdate from './components/Community/CommunityUpdate';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Main />}></Route>
        <Route path='/notice' element={<Notice />}></Route>
        <Route path='/community' element={<Community />}></Route>
        <Route path='/rank' element={<Rank />}></Route>
        <Route path='/signup' element={<SignUp />}></Route>
        <Route path='/community/:id' element={<CommunityDetail />}></Route>
        <Route path='/community/create' element={<CommunityCreate />}></Route>
        <Route path='/community/:id/update' element={<CommunityUpdate />}></Route>
        <Route path='/gameGuide/:name' element={<GameGuide />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
