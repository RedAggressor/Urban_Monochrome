import '../NavLinks/navLinks.scss';

interface Props {
  className: string;
}

export const NavLinks: React.FC<Props> = ({ className }) => {
  return (
    <nav className={className}>
        <a href="#" className="nav_link">Shop</a>
        <a href="#" className="nav_link">New</a>
        <a href="#" className="nav_link">Sale</a>
        </nav>
  )
}
