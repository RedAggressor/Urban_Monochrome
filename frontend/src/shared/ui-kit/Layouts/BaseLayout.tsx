import { ReactNode } from 'react';
import { Outlet } from 'react-router-dom';

interface LayoutSockets {
  headerSlot: ReactNode;
  footerSlot: ReactNode;
}

export const BaseLayout = ({ headerSlot, footerSlot }: LayoutSockets) => {
  return (
    <>
      {headerSlot}
      <main>
        <Outlet />
      </main>
      {footerSlot}
    </>
  );
};
