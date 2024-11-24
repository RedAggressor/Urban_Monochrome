import { Route, Routes } from "react-router-dom";
import { HomePage } from "../../pages/HomePage.tsx/HomePage";

export const AppRouter = () => (
  <Routes>
      <Route index element={<HomePage />} />
      </Routes>
) 