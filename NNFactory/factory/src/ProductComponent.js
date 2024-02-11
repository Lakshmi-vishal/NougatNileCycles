import React, { useState, useEffect, useMemo } from 'react';
import axios from 'axios';
import { makeStyles } from '@material-ui/core/styles';
import {
    IconButton,
    Badge,
    TextField,
    Paper,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import RemoveIcon from '@material-ui/icons/Remove';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import ArrowUpwardIcon from '@material-ui/icons/ArrowUpward';
import ArrowDownwardIcon from '@material-ui/icons/ArrowDownward';
import { useNavigate } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
    table: {
        minWidth: 650,
    },
    tableContainer: {
        borderRadius: 15,
        margin: theme.spacing(2),
        maxWidth: '95%',
    },
    tableHeaderCell: {
        fontWeight: 'bold',
        backgroundColor: theme.palette.primary.dark,
        color: theme.palette.getContrastText(theme.palette.primary.dark),
    },
    title: {
        padding: theme.spacing(2),
        color: theme.palette.primary.dark,
    },
    quantity: {
        display: 'flex',
        alignItems: 'center',
    },
    toolbar: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: theme.spacing(2),
    },
    leftToolbar: {
        display: 'flex',
        alignItems: 'center',
        gap: theme.spacing(2),
    },
    filterInput: {
        [theme.breakpoints.up('sm')]: {
            width: 300,
        },
    },
}));

const ProductList = () => {
    const classes = useStyles();
    const navigate = useNavigate();
    const [products, setProducts] = useState([]);
    const [cart, setCart] = useState({});
    const [sortConfig, setSortConfig] = useState({ key: null, direction: 'asc' });
    const [filter, setFilter] = useState('');

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await axios.get('http://localhost:5200/api/Products/');
                setProducts(response.data);
            } catch (error) {
                console.error('Failed to fetch products', error);
            }
        };
        fetchProducts();
    }, []);

    const requestSort = (key) => {
        let direction = 'asc';
        if (sortConfig.key === key && sortConfig.direction === 'asc') {
            direction = 'desc';
        }
        setSortConfig({ key, direction });
    };

    const sortedProducts = useMemo(() => {
        let sortableProducts = [...products];
        if (sortConfig.key) {
            sortableProducts.sort((a, b) => {
                if (a[sortConfig.key] < b[sortConfig.key]) {
                    return sortConfig.direction === 'asc' ? -1 : 1;
                }
                if (a[sortConfig.key] > b[sortConfig.key]) {
                    return sortConfig.direction === 'asc' ? 1 : -1;
                }
                return 0;
            });
        }
        return sortableProducts;
    }, [products, sortConfig]);

    const filteredProducts = useMemo(() => {
        return sortedProducts.filter((product) =>
            product.name.toLowerCase().includes(filter)
        );
    }, [sortedProducts, filter]);

    const handleFilterChange = (event) => {
        setFilter(event.target.value.toLowerCase());
    };

    const addToCart = (productId) => {
        setCart((currentCart) => {
            const newQuantity = (currentCart[productId] || 0) + 1;
            return { ...currentCart, [productId]: newQuantity };
        });
    };

    const removeFromCart = (productId) => {
        setCart((currentCart) => {
            const newQuantity = (currentCart[productId] || 0) - 1;
            if (newQuantity <= 0) {
                const updatedCart = { ...currentCart };
                delete updatedCart[productId];
                return updatedCart;
            }
            return { ...currentCart, [productId]: newQuantity };
        });
    };

    return (
        <>
            <div className={classes.toolbar}>
                <div className={classes.leftToolbar}>
                    <IconButton onClick={() => navigate(-1)} color="inherit">
                        <ArrowBackIcon />
                    </IconButton>
                    <TextField
                        className={classes.filterInput}
                        label="Filter Products"
                        variant="outlined"
                        onChange={handleFilterChange}
                    />
                </div>
                <Badge badgeContent={Object.values(cart).reduce((acc, curr) => acc + curr, 0)} color="secondary">
                    <ShoppingCartIcon />
                </Badge>
            </div>
            <TableContainer component={Paper} className={classes.tableContainer}>
                <Typography variant="h4" className={classes.title}>
                    Products
                </Typography>
                <Table className={classes.table} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell className={classes.tableHeaderCell}>
                                Product ID
                                <IconButton onClick={() => requestSort('productId')} size="small">
                                    {sortConfig.key === 'productId' && sortConfig.direction === 'asc' ? <ArrowDownwardIcon /> : <ArrowUpwardIcon />}
                                </IconButton>
                            </TableCell>
                            <TableCell className={classes.tableHeaderCell}>
                                Name
                                <IconButton onClick={() => requestSort('name')} size="small">
                                    {sortConfig.key === 'name' && sortConfig.direction === 'asc' ? <ArrowDownwardIcon /> : <ArrowUpwardIcon />}
                                </IconButton>
                            </TableCell>
                            <TableCell className={classes.tableHeaderCell}>
                                Product Number
                                <IconButton onClick={() => requestSort('productNumber')} size="small">
                                    {sortConfig.key === 'productNumber' && sortConfig.direction === 'asc' ? <ArrowDownwardIcon /> : <ArrowUpwardIcon />}
                                </IconButton>
                            </TableCell>
                            <TableCell className={classes.tableHeaderCell}>
                                Quantity
                                <IconButton onClick={() => requestSort('quantity')} size="small">
                                    {sortConfig.key === 'quantity' && sortConfig.direction === 'asc' ? <ArrowDownwardIcon /> : <ArrowUpwardIcon />}
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredProducts.map((product) => (
                            <TableRow key={product.productId}>
                                <TableCell component="th" scope="row">
                                    {product.productId}
                                </TableCell>
                                <TableCell>{product.name}</TableCell>
                                <TableCell>{product.productNumber}</TableCell>
                                <TableCell className={classes.quantity}>
                                    <IconButton onClick={() => removeFromCart(product.productId)} size="small">
                                        <RemoveIcon />
                                    </IconButton>
                                    {cart[product.productId] || 0}
                                    <IconButton onClick={() => addToCart(product.productId)} size="small">
                                        <AddIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
};

export default ProductList;
