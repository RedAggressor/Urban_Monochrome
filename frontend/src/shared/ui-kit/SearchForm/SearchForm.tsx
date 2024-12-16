import { ReactNode } from 'react';
import '../../ui-kit/SearchForm/searchInput.scss'

interface Props {
  placeholder: string;
  icon: ReactNode;
};

export const SearchForm: React.FC<Props> = ({ placeholder, icon }) => {
  return (
    <form action="" method="post" className="header__search">
      <input
          type="text"
          placeholder={placeholder}
          className="search__input"
        />
        <button type="submit" className="search__icon">{icon}</button>
      </form>
  )
}
